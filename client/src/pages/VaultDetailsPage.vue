<script setup>
import { AppState } from "@/AppState";
import KeepCard from "@/components/globals/KeepCard.vue";
import KeepDetails from "@/components/globals/KeepDetails.vue";
import ModalWrapper from "@/components/ModalWrapper.vue";
import { vaultsService } from "@/services/VaultsService";
import Pop from "@/utils/Pop";
import { computed, onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";

const vault = computed(() => AppState.activeVault)
const vaultKeeps = computed(() => AppState.keeps)
const account = computed(() => AppState.account)
const route = useRoute()
const router = useRouter()

onMounted(() => {
  getVaultById(route.params.vaultId)
  getVaultKeepsByVaultId(route.params.vaultId)
})

async function getVaultById(vaultId) {
  try {
    await vaultsService.getVaultById(vaultId)
  } catch (error) {
    Pop.error(error);
    if (error.response.data.includes("No Vault found")) router.push({ name: "Home" })
  }
}

async function getVaultKeepsByVaultId(vaultId) {
  try {
    await vaultsService.getVaultKeepsByVaultId(vaultId)
  } catch (error) {
    Pop.error(error);
  }
}

async function destroyVault(vaultId) {
  try {
    const confirm = await Pop.confirm("Are you sure you want to delete this Vault?", "This will be permanent", "Yes, delete this Vault")
    if (!confirm) return
    await vaultsService.destroyVault(vaultId)
    Pop.success(`${vault.value.name} was successfully deleted`)
    router.push({ name: "Profile", params: { profileId: account.value.id } })
  } catch (error) {
    Pop.error(error);
  }
}
</script>


<template>
  <div v-if="vault" class="container">
    <div class="row mt-5 justify-content-center">
      <div class="col-5 rounded vault-img shadow">
        <div class="row justify-content-center align-items-end h-100">
          <div class="col-10 px-0 text-center">
            <p class="display-4 text-uppercase text-merriweather-bold text-light text-outline">{{ vault.name }}</p>
            <p class="text-light fs-5 text-merriweather-bold">by {{ vault.creator.name }}</p>
          </div>
        </div>
      </div>
      <div class="row justify-content-center">
        <div class="col-5 text-end px-0">
          <div v-if="account?.id == vault.creatorId" class="dropdown mt-4">
            <button class="btn delete-vault-btn rounded-pill" type="button" data-bs-toggle="dropdown"
              aria-expanded="false">
              <i role="button" class="mdi px-1 mdi-dots-horizontal fs-2"></i>
            </button>
            <ul class="dropdown-menu py-1">
              <li>
                <p class="dropdown-item m-0 " data-bs-toggle="modal" data-bs-target="#newKeepFormModal">
                  Edit Vault
                </p>
              </li>
              <hr class="m-1">
              <li>
                <p @click="destroyVault(vault.id)" class="dropdown-item m-0 text-danger">Delete Vault</p>
              </li>
            </ul>
          </div>
        </div>
      </div>
      <div class="row justify-content-center mb-4">
        <div class="col-3 d-flex justify-content-center">
          <div class="text-center rounded-pill bg-secondary px-2">
            <p class="text-merriweather fs-4 px-1 mb-0">{{ vaultKeeps.length }} {{ vaultKeeps.length == 1 ? "Keep" :
              "Keeps" }}</p>
          </div>
        </div>
      </div>
      <div class="row mb-3">
        <div class="masonry">
          <KeepCard v-for="keep in vaultKeeps" :key="keep.id" :keep="keep" :onProfile="false" />
        </div>
      </div>
    </div>
  </div>
  <ModalWrapper modal-id="keepDetailsModal" modal-size="modal-xl modal-fullscreen-sm-down">
    <KeepDetails />
  </ModalWrapper>
</template>


<style lang="scss" scoped>
.vault-img {
  background-image: v-bind('vault?.imgUrl');
  height: 40dvh;
  background-size: cover;
  background-position: center;
}

.text-outline {
  -webkit-text-stroke-width: 1px;
  -webkit-text-stroke-color: #040404;
  margin: 0;
}

.delete-vault-btn {
  background-color: var(--bs-page);
  border: none;

  &:hover {
    background-color: #a8a8a856;
  }
}

li>p {
  user-select: none;
}

p {
  -webkit-text-stroke-width: .5px;
  -webkit-text-stroke-color: #040404;
  overflow-wrap: break-word;
  margin: 0;
  overflow: hidden;
  max-height: 3.5em;
}

.masonry {
  column-gap: 1em;
  column-fill: balance-all;

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