<script setup>
import { AppState } from '@/AppState.js';
import { keepsService } from '@/services/KeepsService.js';
import { vaultKeepsService } from '@/services/VaultKeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Modal } from 'bootstrap';
import { computed, ref } from 'vue';



const keep = computed(() => AppState.Keep)
const vaults = computed(() => AppState.vaults)

const vaultkeepData = ref({
    vaultId: 0,
    keepId: 0,
})

function closeModal() {
    Modal.getOrCreateInstance('#KeepDetailsModal').hide()
}


async function createVaultKeep() {
    vaultkeepData.value.keepId = keep.value.id
    vaultKeepsService.createVaultKeep(vaultkeepData.value)
    // logger.log(vaultkeepData.value)
    closeModal()
}

async function deleteVaultKeep(keepId) {
    vaultKeepsService.deleteVaultKeep(keepId)
}

</script>


<template>
    <div id="KeepDetailsModal" class="modal fade">
        <div class="modal-dialog modal-size modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body p-0">
                    <div class="row" v-if="AppState.Keep">
                        <div class="col-6 p-0">
                            <img :src="keep.img" alt="" class="modal-image rounded-start">
                        </div>
                        <div class="col-6 bg-light-creme rounded-end">
                            <div class="d-flex justify-content-between align-items-center flex-column h-100">
                                <div class=" d-flex justify-content-center">
                                    <div class="me-4"><span class="mdi mdi-eye-outline me-1"></span>{{ keep.views }}
                                    </div>
                                    <div><span class="mdi mdi-alpha-k-box-outline me-1"></span>{{ keep.kept }}</div>
                                </div>
                                <div class="row text-center">
                                    <div class="fs-2">{{ keep.name }}</div>
                                    <p class="">{{ keep.description }}</p>
                                </div>
                                <div class="d-flex justify-content-between align-items-center w-100">
                                    <form @submit.prevent="createVaultKeep"
                                        v-if="AppState.account && vaults.length && !AppState.keepInVault"
                                        class="d-flex">
                                        <select v-model="vaultkeepData.vaultId" id="keep-vaultkeep mb-5 vault-picker"
                                            class="vault-picker">
                                            <option v-for="vault in vaults" :key="vault.id" :value="vault.id">{{
                                                vault.name }}</option>
                                        </select>
                                        <button type="submit" class="btn btn-lavender ms-3">save</button>
                                    </form>
                                    <div v-if="AppState.keepInVault">
                                        <button @click="deleteVaultKeep(AppState.vaultKeep)"
                                            class="btn btn-lavender-outline remove-vault-button">remove from
                                            vault</button>
                                    </div>
                                    <router-link @click="closeModal()"
                                        :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
                                        <div class="d-flex align-items-center mb-2">
                                            <img class="profile-picture me-1" :src="keep.creator.picture" alt="">
                                            <div class="text-dark fw-bold">{{ keep.creator.name }}</div>
                                        </div>
                                    </router-link>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</template>


<style lang="scss" scoped>
.modal-image {
    width: 100%;
    height: 100%;
    object-fit: cover;
}

.modal-size {
    width: 80%;
    max-width: 80%;
    height: 80%;
    max-height: 80%;
}

* {
    text-decoration: inherit;
}

.vault-picker {
    width: 200px;
    background: #FEF6F0;
    border: none;
}

.remove-vault-button {
    border-bottom: 5px;
    border-style: inset;
    border-color: black;
}
</style>