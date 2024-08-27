<script setup>
import { AppState } from "@/AppState";
import KeepCard from "@/components/globals/KeepCard.vue";
import VaultCard from "@/components/globals/VaultCard.vue";
import { profilesService } from "@/services/ProfilesService";
import Pop from "@/utils/Pop";
import { computed, onMounted } from "vue";
import { useRoute } from "vue-router";

const profile = computed(() => AppState.focusedProfile)
const profileKeeps = computed(() => AppState.profileKeeps)
const profileVaults = computed(() => AppState.profileVaults)
const route = useRoute()

onMounted(() => {
  getProfileData()
  getProfileKeeps()
  getProfileVaults()
})

async function getProfileData() {
  try {
    await profilesService.getProfileData(route.params.profileId)
  } catch (error) {
    Pop.error(error);
  }
}

async function getProfileKeeps() {
  try {
    await profilesService.getProfileKeeps(route.params.profileId)
  } catch (error) {
    Pop.error(error);
  }
}

async function getProfileVaults() {
  try {
    await profilesService.getProfileVaults(route.params.profileId)
  } catch (error) {
    Pop.error(error);
  }
}
</script>


<template>
  <div v-if="profile" class="container">
    <section class="row justify-content-center mt-5 mb-3">
      <div class="col-10 mb-5">
        <div class="cover-img shadow rounded position-relative">
          <div class="row">
            <div class="col-4 text-center profile-info">
              <img class="profile-img shadow" :src="profile.picture" :alt="profile.name">
              <p class="fs-3 fw-bold mb-0">{{ profile.name }}</p>
              <p class="fw-semibold">{{ profileVaults.length }} Vaults | {{ profileKeeps.length }} Keeps</p>
            </div>
          </div>
        </div>
      </div>
      <div class="col-12 my-5"></div>
      <div class="col-12 mt-5 mb-3">
        <div class="row">
          <div class="col-12">
            <p class="fs-2 fw-semibold">Vaults</p>
          </div>
          <div v-for="vault in profileVaults" :key="vault.id" class="col-md-3 col-sm-4 col-6">
            <VaultCard :vault="vault" />
          </div>
        </div>
      </div>
      <div class="col-12">
        <div class="row jutstify-content-center">
          <div class="col-12">
            <p class="fs-2 fw-semibold">Keeps</p>
          </div>
          <div class="masonry">
            <KeepCard v-for="keep in profileKeeps" :key="keep.id" :keep="keep" :onProfile="true" />
          </div>
        </div>
      </div>

    </section>
  </div>
</template>


<style lang="scss" scoped>
.cover-img {
  background-image: v-bind('profile?.coverImgUrl');
  background-size: cover;
  background-position: center;
  height: 45dvh;
}

.profile-img {
  height: 20dvh;
  aspect-ratio: 1/1;
  border-radius: 50%;
}

.profile-info {
  position: absolute;
  bottom: -45%;
  left: 50%;
  transform: translate(-50%);
}

.masonry {
  column-gap: 1em;
  column-fill: balance;

  &>* {
    display: inline-block;
  }
}

@media screen and (max-width: 767.98px) {
  .masonry {
    columns: 2;
  }
}

@media screen and (min-width: 768px) {
  .masonry {
    columns: 3;
  }
}

@media screen and (min-width: 991.98px) {
  .masonry {
    columns: 4;
  }
}
</style>