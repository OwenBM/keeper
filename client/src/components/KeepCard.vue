<script setup>
import { AppState } from '@/AppState.js';
import { Keep, VaultKeep } from '@/models/Keep.js';
import { keepsService } from '@/services/KeepsService.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed } from 'vue';




defineProps({
    keep: { type: Keep, required: true },
    vaultKeepId: { type: Number, required: false }
})

const account = computed(() => AppState.account)

async function getKeepById(keepId) {
    await keepsService.getKeepById(keepId)
}

async function deleteKeep(keepId) {
    // logger.log("button!", keepId)
    await Pop.confirm("are you sure you want to delete your keep?")
    keepsService.deleteKeep(keepId)

}

</script>


<template>
    <div class="hover-craft">
        <div class="keep-card position-relative mb-4 shadow rounded item">
            <div data-bs-toggle="modal" data-bs-target="#KeepDetailsModal" @click="getKeepById(keep.id)">
                <img :src="keep.img" alt="" class="img-card rounded">
                <div class="keep-name text-white fs-4 ms-1 shadow">
                    {{ keep.name }}
                </div>
                <div class="keep-profile" v-if="keep.creator">
                    <img class="profile-picture m-2 shadow-heavier" :src="keep.creator.picture" alt="">
                </div>
            </div>
            <div v-if="keep.creatorId == account?.id" @click="deleteKeep(keep.id)" class="delete-button"> <span
                    class="bg-red text-white px-2 hover circle">x</span></div>
        </div>
    </div>

</template>


<style lang="scss" scoped>
.hover-craft {
    &:hover {
        transform: scale(1.05);
        transition: .5s;
        // transition-duration: .5s;
    }
}

.img-card {
    height: 40%;
    width: 100%;
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

.delete-button {
    position: absolute;
    top: 0;
    right: 0;
}

.delete-button:hover {
    cursor: pointer;
}

.circle {
    border-radius: 50%;
}

.shadow-heavier {
    box-shadow: 1px 1px 10px #000000;
}
</style>