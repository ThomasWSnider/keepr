<script setup>
import { computed, onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import { AppState } from "@/AppState.js";
import { useRouter } from "vue-router";

const account = computed(() => AppState.account)
const router = useRouter()

function pushToProfile() {
  router.push({ name: 'Profile', params: { profileId: account.value.id } })
}



</script>

<template>
  <nav class="navbar navbar-expand-sm bg-transparent shadow px-3">
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <button v-if="account" @click="pushToProfile" class="btn btn-secondary rounded-pill fw-semibold">
            My Profile
          </button>
        </li>
        <li>
          <div v-if="account" class="dropdown">
            <button class="btn btn-secondary-outline dropdown-toggle fw-semibold" type="button"
              data-bs-toggle="dropdown" aria-expanded="false">
              Create
            </button>
            <ul class="dropdown-menu py-1">
              <li>
                <p class="dropdown-item m-0 " data-bs-toggle="modal" data-bs-target="#newKeepFormModal">
                  New Keep
                </p>
              </li>
              <hr class="m-1">
              <li>
                <p class="dropdown-item m-0" data-bs-toggle="modal" data-bs-target="#newVaultFormModal">New Vault
                </p>
              </li>
            </ul>
          </div>
        </li>
      </ul>
    </div>
    <router-link class="navbar-brand d-flex position-absolute" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img alt="logo" src="/img/keeprlogo.png" height="100" />
      </div>
    </router-link>
    <!-- LOGIN COMPONENT HERE -->
    <Login />
  </nav>
</template>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

a {
  left: 50%;
  transform: translate(-50%)
}

@media screen and (min-width: 576px) {
  nav {
    height: 64px;
  }
}

li {
  p {
    user-select: none;
  }
}
</style>
