<script setup>
import { computed, ref } from 'vue';
import { AppState } from '../AppState.js';
import Pop from "@/utils/Pop.js";
import { accountService } from "@/services/AccountService.js";

const account = computed(() => AppState.account)

const editableAccountData = ref({
  name: account.value.name,
  picture: account.value.picture,
  coverImg: account.value.coverImg
})

async function editAccountInfo() {
  try {
    const confirm = await Pop.confirm("Are you sure you wish to update your Account information?")
    if (!confirm) return
    await accountService.editAccountInfo(editableAccountData.value)
    editableAccountData.value = {
      name: account.value.name,
      picture: account.value.picture,
      coverImg: account.value.coverImg
    }
  } catch (error) {
    Pop.error(error)
  }
}

</script>

<template>
  <div v-if="account" class="container">
    <section class="row justify-content-center mt-5 mb-3">
      <div class="col-10">
        <div class="cover-img shadow rounded">
          <div class="row justify-content-center">
            <div class="col-10 text-center profile-info mt-5">
              <img class="profile-img shadow mt-lg-3" :src="account.picture" :alt="account.name">
              <p class="fs-1 fw-bold mb-0 text-light">{{ account.name }}</p>
            </div>
          </div>
        </div>
      </div>
      <form @submit.prevent="editAccountInfo()" class="col-10 mt-3 mb-5">
        <section class="row justify-content-between align-items-center">
          <div class="col-lg-6 mb-3">
            <label for="name">Name</label>
            <input v-model="editableAccountData.name" type="text" class="form-control" id="name" placeholder="Name">
          </div>
          <div class="col-lg-6 mb-3">
            <label for="picture">Picture</label>
            <input v-model="editableAccountData.picture" type="url" class="form-control" id="picture"
              placeholder="Picture">
          </div>
          <div class="col-lg-6 mb-3">
            <label for="coverImg">Cover Image</label>
            <input v-model="editableAccountData.coverImg" type="url" class="form-control" id="coverImg"
              placeholder="Cover Image">
          </div>
          <div class="col-lg-6 mb-3 py-0">
            <div class="d-grid">
              <button class="btn btn-secondary mt-4 py-lg-0 fw-semibold" type="submit">Submit Changes</button>
            </div>
          </div>
        </section>
      </form>
    </section>
  </div>
</template>

<style scoped lang="scss">
.cover-img {
  background-image: v-bind('account?.coverImgUrl');
  background-size: cover;
  background-position: center;
  height: 45dvh;
}

.profile-info {
  -webkit-text-stroke-width: 2px;
  -webkit-text-stroke-color: #040404;
}

.profile-img {
  height: 20dvh;
  aspect-ratio: 1/1;
  border-radius: 50%;
}

button {
  height: 38px;
}
</style>
