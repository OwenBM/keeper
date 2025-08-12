<script setup>
import CreateAlbumModal from './components/CreateAlbumModal.vue';
import CreateKeepModal from './components/CreateKeepModal.vue';
import KeepDetailsModal from './components/KeepDetailsModal.vue';
import Navbar from './components/Navbar.vue';
import { RouterView } from 'vue-router';

</script>

<template>
  <header class="sticky-top">
    <Navbar />
  </header>
  <main>
    <RouterView />
  </main>
  <footer class=" text-center">
    Made with <i class="mdi mdi-heart text-pink"></i> by CodeWorks
  </footer>
  <KeepDetailsModal />
  <CreateAlbumModal />
  <CreateKeepModal />
</template>

<style lang="scss">
header,
main,
footer {
  background-color: #FEF6F0;
}
</style>
