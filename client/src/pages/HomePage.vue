<script setup>
import { AppState } from "@/AppState";
import { keepsService } from "@/services/KeepsService";
import Pop from "@/utils/Pop";
import { computed, onMounted } from "vue";


const keeps = computed(() => AppState.keeps)

onMounted(() => {
  getAllKeeps()
})

async function getAllKeeps() {
  try {
    await keepsService.getAllKeeps()
  }
  catch (error) {
    Pop.error(error);
  }
}

</script>

<template>
  <div class="container">
    <section class="row">
      <div v-for="keep in keeps" :key="keep.id" class="col-3">
        {{ keep }}
      </div>
    </section>
  </div>
</template>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: clamp(500px, 50vw, 100%);

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
