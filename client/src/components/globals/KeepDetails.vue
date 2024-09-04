<script setup>
import { AppState } from "@/AppState";
import { vaultKeepsService } from "@/services/VaultKeepsService";
import Pop from "@/utils/Pop";
import { Modal } from "bootstrap";
import { computed, ref } from "vue";
import { useRoute } from "vue-router";
import Image from "./Image.vue";

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
  <div v-if="keep" class="modal-body ps-md-0 px-3 container-fluid pt-1 py-md-0 position-relative">
    <section class="row align-items-between">
      <Image class="img-fluid d-none d-md-block rounded-start keep-img p-0" :image="keep.img" :alt="keep.name" />
      <div class="col-12 d-block d-md-none p-0 mobile-img">
        <Image class="img-fluid d-block d-md-none p-0 rounded-top" :image="keep.img" :alt="keep.name" />
      </div>

      <div class="col-md-6 col-12">

        <div class="d-flex keep-details flex-column justify-content-between m4-4 h-100">

          <div class="d-flex justify-content-center mt-3">
            <p class="fw-semibold fs-5 mb-0 me-5 text-secondary" :title="`${keep.views} Views`"><i
                class="mdi mdi-eye-outline"></i> {{ keep.views }}
            </p>
            <p class="fw-semibold fs-5 mb-0 text-secondary" :title="`${keep.kept} Keeps`">
              <i class="mdi mdi-alpha-k-box-outline"></i> {{ keep.kept }}
            </p>
          </div>

          <div>
            <p class="fs-2 fw-bold text-center">{{ keep.name }}</p>
            <p class="px-md-4">{{ keep.description }}</p>
          </div>



          <div class="d-flex justify-content-between align-items-center mb-3">

            <form @submit.prevent="saveKeep(keep)" v-if="account && route.name != 'Vault'">
              <div class="d-flex">
                <select v-model="editableVaultKeepData.vaultId" class="form-select rounded-end-0"
                  aria-label="Select Vault" required>
                  <option selected disabled value="">--Select Vault--</option>
                  <option v-for="vault in vaults" :key="vault.id" :value="vault.id">{{ vault.name }}</option>
                </select>
                <button type="submit" :title="`Save ${keep.name} to one of your vaults`"
                  class="btn btn-secondary rounded-start-0 px-2 py-0">Save</button>
              </div>
            </form>


            <div v-else-if="route.name == 'Vault' && account.id == activeVault?.creatorId" class="text-center">
              <p @click="destroyVaultKeep(keep.vaultKeepId)" :title="`Remove ${keep.name} from ${activeVault.name}`"
                class="fw-semibold fs-5 m-0 remove-keep-btn">
                <i class="mdi mdi-cancel"></i>Remove Keep
              </p>
            </div>

            <div class="d-flex align-items-center justify-content-center">
              <RouterLink :title="`Go to ${keep.creator.name}'s profile page`" data-bs-toggle="modal"
                data-bs-target="#keepDetailsModal" :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
                <Image class="creator-img selectable me-1" :image="keep.creator.picture" :alt="keep.creator.name" />
              </RouterLink>
              <p class="mb-0 fw-semibold d-md-none d-lg-block">{{ keep.creator.name }}</p>
            </div>
          </div>


          <button class="d-block d-sm-none btn btn-secondary mb-3" title="Close" data-bs-toggle="modal"
            data-bs-target="#keepDetailsModal">Close</button>

        </div>
      </div>
    </section>
  </div>
</template>


<style lang="scss" scoped>
.keep-img {
  min-height: 75dvh;
  max-height: 90dvh;
  width: 50%;
  padding-left: 0;
  object-fit: cover;

  @media screen and (max-width: 767.98px) {
    min-height: 50dvh;
  }
}

.creator-img {
  width: 6dvh;
  height: 6dvh;
  aspect-ratio: 1/1;
  border-radius: 50%;
}

.keep-details {
  min-height: 75dvh;
  max-height: 90dvh;

  @media screen and (max-width: 767.98px) {
    min-height: 50dvh;
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

.modal-body {
  --bs-modal-padding: 0;
}

p {
  overflow-wrap: break-word;
  overflow-y: hidden;
}
</style>