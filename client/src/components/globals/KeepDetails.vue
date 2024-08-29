<script setup>
import { AppState } from "@/AppState";
import { vaultKeepsService } from "@/services/VaultKeepsService";
import Pop from "@/utils/Pop";
import { Modal } from "bootstrap";
import { computed, ref } from "vue";
import { useRoute } from "vue-router";

const account = computed(() => AppState.account)
const activeVault = computed(() => AppState.activeVault)
const vaults = computed(() => AppState.accountVaults)
const keep = computed(() => AppState.activeKeep)
const route = useRoute()

const editableVaultKeepData = ref({
  vaultId: "",
})

async function saveKeep(keep) {
  try {
    const success = await vaultKeepsService.saveKeep(editableVaultKeepData.value, keep)
    editableVaultKeepData.value = {
      vaultId: "",
    }
    Pop.success(success)
  } catch (error) {
    Pop.error(error);
  }
}

async function destroyVaultKeep(vaultKeepId) {
  try {
    const confirm = await Pop.confirm("Are you sure you want to delete this keep from your Vault?", "", "Confirm")
    if (!confirm) return
    const success = await vaultKeepsService.destroyVaultKeep(vaultKeepId)
    Modal.getOrCreateInstance('#keepDetailsModal').hide()
    Pop.success(success)
  } catch (error) {
    Pop.error(error);
  }
}
</script>


<template>
  <div v-if="keep" class="modal-body px-3 pe-lg-3 container-fluid p-0 position-relative">
    <section class="row align-items-between">
      <img class="img-fluid rounded-start keep-img position-absolute" :src="keep.img" :alt="keep.name">
      <div class="col-md-6 col-12 p-0 space"></div>
      <div class="col-md-6 col-12 keep-details">
        <div class="row justify-content-center align-items-between mt-4">
          <div class="col-12 d-flex justify-content-center">
            <p class="fw-semibold fs-5 mb-0 me-5 text-secondary"><i class="mdi mdi-eye-outline"></i> {{ keep.views }}
            </p>
            <p class="fw-semibold fs-5 mb-0 text-secondary"><i class="mdi mdi-alpha-k-box-outline"></i> {{
              keep.kept
            }}
            </p>
          </div>
          <p class="fs-2 fw-bold text-center">{{ keep.name }}</p>
          <p class="px-4">{{ keep.description }}</p>
        </div>
        <div class="row justify-content-between align-items-center mb-3">

          <form @submit.prevent="saveKeep(keep)" class="col-10 col-lg-7 mx-auto mb-3 m-lg-0"
            v-if="account && route.name != 'Vault'">
            <div class="d-flex">
              <select v-model="editableVaultKeepData.vaultId" class="form-select rounded-end-0"
                aria-label="Select Vault" required>
                <option selected disabled value="">--Select Vault--</option>
                <option v-for="vault in vaults" :key="vault.id" :value="vault.id">{{ vault.name }}</option>
              </select>
              <button type="submit" class="btn btn-secondary rounded-start-0 ps-1 pe-2 py-0">Save</button>
            </div>
          </form>
          <div v-else-if="route.name == 'Vault' && account.id == activeVault?.creatorId" class="col-7 text-center">
            <p @click="destroyVaultKeep(keep.vaultKeepId)" class="fw-semibold fs-5 m-0 remove-keep-btn"><i
                class="mdi mdi-cancel"></i>
              Remove Keep</p>
          </div>
          <div class="col-12 col-lg-5 pe-1 mb-3 d-flex align-items-center justify-content-center">
            <RouterLink :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
              <img class="creator-img selectable me-1" :src="keep.creator.picture" :alt="keep.creator.name"
                :title="`Go to ${keep.creator.name}'s profile page`" data-bs-toggle="modal"
                data-bs-target="#keepDetailsModal">
            </RouterLink>
            <p class="mb-0 fw-semibold">{{ keep.creator.name }}</p>
          </div>
          <div class="col-10 d-grid d-lg-none mx-auto">
            <button class="btn btn-secondary" data-bs-toggle="modal" data-bs-target="#keepDetailsModal">Close</button>
          </div>
          <div>
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
  height: 100%;
  width: 50%;
  padding-left: 0;
  object-fit: cover;
  left: 0;
  top: 0;

  @media screen and (max-width: 767.98px) {
    height: 33dvh;
    width: 100%;
    padding: 0;
  }
}

.space {
  @media screen and (max-width: 767.98px) {
    height: 33dvh;
  }
}

.keep-details {
  @media screen and (max-width: 767.98px) {
    height: 66dvh;

    .row {
      height: 30dvh;
    }
  }
}

.remove-keep-btn {
  color: var(--bs-secondary);
  user-select: none;
  cursor: pointer;

  &:hover {
    text-decoration: underline;
  }

  &:active {
    text-decoration: underline;
    filter: hue-rotate(330deg);
  }
}

p {
  overflow-wrap: break-word;
  overflow-y: hidden;
}
</style>