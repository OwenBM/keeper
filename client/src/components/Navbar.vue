<script setup>
import { computed } from 'vue';
import Login from './Login.vue';
import { AppState } from '@/AppState.js';


const account = computed(() => AppState.account)

</script>

<template>
  <nav class="navbar navbar-expand-md border roboto">
    <div class="container gap-2">
      <div class="d-flex justify-content-between w-100 align-items-center">
        <div class="d-flex">
          <RouterLink :to="{ name: 'Home' }" class="d-flex align-items-center text-light">
            <button class="fs-5 btn btn-creme rounded-pill">Home</button>
          </RouterLink>
          <div v-if="account" class="dropdown">
            <!-- <div class="bg-dark" :class="$route.name != 'Home'"> -->
            <div v-if="$route.name == 'Home'">
              <div class="dropdown ms-3">
                <button class="btn btn-creme-outline fs-5 dropdown-toggle" type="button" data-bs-toggle="dropdown"
                  aria-expanded="false">
                  Create
                </button>
                <ul class="dropdown-menu fs-4 text-center">
                  <div class="hover-jank border-bottom" data-bs-toggle="modal" data-bs-target="#CreateVaultModal">
                    Vault
                  </div>
                  <div class="hover-jank" data-bs-toggle="modal" data-bs-target="#CreateKeepModal">
                    Keep
                  </div>
                </ul>
              </div>
            </div>
          </div>
        </div>
        <!-- collapsing menu -->
        <!-- <div class="collapse navbar-collapse d-flex justify-content-between" id="navbar-links"> -->
        <RouterLink :to="{ name: 'Home' }" class="d-flex align-items-center text-light">
          <img class="logo-jargin" src="../../public/img/Keepr logo.png" alt="">
        </RouterLink>
        <Login />
      </div>
      <!-- </div> -->
    </div>
  </nav>
</template>

<style lang="scss" scoped>
a {
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

.logo-jargin {
  height: 60px;
  // margin-left: 5vw;
}

.hover-jank {
  cursor: pointer;
}
</style>
