<script setup>
import { Keep } from '@/models/Keep.js';
import { keepsService } from '@/services/KeepsService.js';




defineProps({
    keep: { type: Keep, required: true }
})

async function getKeepById(keepId) {
    await keepsService.getKeepById(keepId)
}

</script>


<template>
    <div class="keep-card position-relative mb-2 gradient" data-bs-toggle="modal" data-bs-target="#KeepDetailsModal"
        @click="getKeepById(keep.id)">
        <img :src="keep.img" alt="" class="img-card">
        <div class="keep-name text-white fs-4 ms-1">
            {{ keep.name }}
        </div>
        <div class="keep-profile">
            <img class="profile-picture m-2" :src="keep.creator.picture" alt="">
        </div>
    </div>

</template>


<style lang="scss" scoped>
.gradient {
    background: #000000;
    background: linear-gradient(0deg, rgba(0, 0, 0, 0.77) 0%, rgba(0, 0, 0, 0) 100%);
}

.img-card {
    height: 40%;
    width: 100%;
    z-index: 0
}

.keep-name {
    position: absolute;
    bottom: 0;
    left: 0;
}

.keep-profile {
    position: absolute;
    bottom: 0;
    right: 0;
}
</style>