<script setup>
import KeepDetailsModal from './components/KeepDetailsModal.vue';
import Navbar from './components/Navbar.vue';
import { RouterView } from 'vue-router';

</script>

<template>
  <header>
    <Navbar />
  </header>
  <main>
    <RouterView />
  </main>
  <footer class=" text-center">
    Made with <i class="mdi mdi-heart text-pink"></i> by CodeWorks
  </footer>
  <KeepDetailsModal modalId="exampleModal" />
</template>

<style lang="scss">
header,
main,
footer {
  background-color: #FEF6F0;
}
</style>
