<script setup>
import { Vault } from "@/models/Vault";

defineProps({ vault: Vault })
</script>


<template>
  <div class="position-relative vault-container mb-3">
    <RouterLink :to="{ name: 'Vault', params: { vaultId: vault.id } }">
      <div class="selectable">
        <img class="img-fluid rounded shadow" :src="vault.img" :alt="vault.name" :title="`View ${vault.name}`">
        <div class=" vault-flavor d-flex align-items-center justify-content-between">
          <p class="fs-2 fw-bold text-light text-meriweather-bold">{{ vault.name }}</p>
        </div>
        <div v-if="vault.isPrivate" class="private-icon">
          <p title="This vault is private"><i class="mdi mdi-lock text-light fs-3"></i></p>
        </div>
      </div>
    </RouterLink>
  </div>
</template>


<style lang="scss" scoped>
.vault-flavor {
  position: absolute;
  width: 66%;
  bottom: 5px;
  left: 10px;
}

img {
  height: 100%;
  object-fit: cover;
  object-position: center;
}

.private-icon {
  position: absolute;
  bottom: 5px;
  right: 10px;
}

p {
  -webkit-text-stroke-width: 1px;
  -webkit-text-stroke-color: #040404;
  overflow-wrap: break-word;
  margin: 0;
  overflow: hidden;
  max-height: 3em;
}
</style>