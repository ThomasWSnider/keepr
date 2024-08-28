<script setup>
import { AppState } from "@/AppState";
import KeepCard from "@/components/globals/KeepCard.vue";
import { vaultsService } from "@/services/VaultsService";
import Pop from "@/utils/Pop";
import { computed, onMounted } from "vue";
import { useRoute } from "vue-router";

const vault = computed(() => AppState.activeVault)
const vaultKeeps = computed(() => AppState.ActiveVaultKeeps)
const route = useRoute()

onMounted(() => {
  getVaultById(route.params.vaultId)
  getVaultKeepsByVaultId(route.params.vaultId)
})

async function getVaultById(vaultId) {
  try {
    await vaultsService.getVaultById(vaultId)
  } catch (error) {
    Pop.error(error);
  }
}

async function getVaultKeepsByVaultId(vaultId) {
  try {
    await vaultsService.getVaultKeepsByVaultId(vaultId)
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
          <i role="button" class="mdi selectable rounded-pill px-1 mdi-dots-horizontal fs-2"></i>
        </div>
      </div>
      <div class="row justify-content-center mb-4">
        <div class="col-3 d-flex justify-content-center">
          <div class="text-center rounded-pill bg-secondary px-2">
            <p class="text-merriweather fs-4 px-1 mb-0">{{ vaultKeeps.length }} Keeps</p>
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

p {
  -webkit-text-stroke-width: .5px;
  -webkit-text-stroke-color: #040404;
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