<script setup>
import { AppState } from "@/AppState";
import { vaultKeepsService } from "@/services/VaultKeepsService";
import Pop from "@/utils/Pop";
import { computed, ref } from "vue";

const account = computed(() => AppState.account)
const vaults = computed(() => AppState.accountVaults)
const keep = computed(() => AppState.activeKeep)

const editableVaultKeepData = ref({
  vaultId: "",
  keepId: keep.value?.id
})

async function saveKeep() {
  try {
    const success = await vaultKeepsService.saveKeep(editableVaultKeepData.value)
    editableVaultKeepData.value = {
      vaultId: "",
      keepId: keep.value?.id
    }
    Pop.success(success)
  } catch (error) {
    Pop.error(error);
  }
}
</script>


<template>
  <div v-if="keep" class="modal-body pe-3 container-fluid p-0">
    <section class="row">
      <div class="col-md-6 col-12 p-0">
        <img class="img-fluid rounded-start keep-img" :src="keep.img" :alt="keep.name">
      </div>
      <div class="col-md-6 col-12 d-flex flex-column justify-content-between">
        <div class="d-flex justify-content-center mt-4">
          <p class="fw-semibold fs-5 mb-0 me-5 text-secondary"><i class="mdi mdi-eye-outline"></i> {{ keep.views }}</p>
          <p class="fw-semibold fs-5 mb-0 text-secondary"><i class="mdi mdi-alpha-k-box-outline"></i> {{ keep.kept }}
          </p>
        </div>
        <div>
          <p class="fs-2 fw-bold text-center">{{ keep.name }}</p>
          <p class="px-4">{{ keep.description }}</p>
        </div>
        <div class="row justify-content-between align-items-center mb-3">

          <form @submit.prevent="saveKeep()" class="col-7" v-if="account">
            <div class="d-flex">
              <select v-model="editableVaultKeepData.vaultId" class="form-select rounded-end-0"
                aria-label="Select Vault">
                <option selected disabled>--Select Vault--</option>
                <option v-for="vault in vaults" :key="vault.id" :value="vault.id">{{ vault.name }}</option>
              </select>
              <button type="submit" class="btn btn-secondary rounded-start-0 ps-1 pe-2 py-0">Save</button>
            </div>
          </form>

          <div class="col-5 pe-1 d-flex align-items-center justify-content-center">
            <img class="creator-img selectable me-1" :src="keep.creator.picture" :alt="keep.creator.name"
              :title="`Go to ${keep.creator.name}'s profile page`">
            <p class="mb-0 fw-semibold">{{ keep.creator.name }}</p>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>


<style lang="scss" scoped>
.creator-img {
  height: 5dvh;
  aspect-ratio: 1/1;
  border-radius: 50%;
}

.keep-img {
  height: 80dvh;
  object-fit: cover;
}
</style>