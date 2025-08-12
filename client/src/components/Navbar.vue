<script setup>
import Login from './Login.vue';




</script>

<template>
  <nav class="navbar navbar-expand-md border">
    <div class="container gap-2">
      <RouterLink :to="{ name: 'Home' }" class="d-flex align-items-center text-light">
        <button class="fs-5 btn btn-secondary">Home</button>
      </RouterLink>
      <!-- collapse button -->
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbar-links"
        aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
        <span class="mdi mdi-menu text-light"></span>
      </button>
      <button class="btn btn-secondary-outline fs-4" data-bs-toggle="modal" data-bs-target="#CreateAlbumModal">
        Album<span class="mdi mdi-plus"></span>
      </button>
      <button class="btn btn-secondary-outline fs-4" data-bs-toggle="modal" data-bs-target="#CreateKeepModal">
        Keep<span class="mdi mdi-plus"></span>
      </button>
      <!-- collapsing menu -->
      <div class="collapse navbar-collapse d-flex justify-content-end" id="navbar-links">
        <Login />
      </div>
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
</style>
