<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import { keepsService } from '@/services/KeepsService.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';

const keeps = computed(() => AppState.Keeps)

onMounted(() => {
  getAllKeeps();
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
  <section class="container-fluid mt-3">
    <div class=" keep-card justify-content-center">
      <div class="" v-for="keep in keeps" :key="'keep-id-' + keep.id" :title="'take a look at ' + keep.name">
        <KeepCard :keep="keep" />
      </div>
    </div>
  </section>
</template>

<style scoped lang="scss">
.keep-card {
  columns: 250px;
  column-gap: 1rem;
}
</style>
