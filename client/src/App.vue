<script setup>
import Navbar from './components/Navbar.vue';
import { AppState } from './AppState.js';
import ModalWrapper from "./components/ModalWrapper.vue";
import NewKeepForm from "./components/globals/NewKeepForm.vue";
import NewVaultForm from "./components/globals/NewVaultForm.vue";


</script>

<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer>

  </footer>
  <ModalWrapper modal-id="newVaultFormModal" modal-size="modal-lg">
    <NewVaultForm />
  </ModalWrapper>
  <ModalWrapper modal-id="newKeepFormModal" modal-size="modal-xl modal-lg">
    <NewKeepForm />
  </ModalWrapper>
</template>

<style lang="scss">
@import '@/assets/scss/main.scss';

:root {
  --main-height: calc(100vh - 32px - 64px);
}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
