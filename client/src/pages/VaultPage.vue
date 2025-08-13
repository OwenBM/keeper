<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { AxiosError } from 'axios';
import { computed, onMounted, onUnmounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();

const vault = computed(() => AppState.vault)
const keeps = computed(() => AppState.vaultKeeps)
const account = computed(() => AppState.account)

onMounted(() => {
    getVaultById();
    getKeepsByVaultId();
    AppState.keepInVault = true
})

onUnmounted(() => {
    AppState.keepInVault = false
})

async function getVaultById() {
    try {
        await vaultsService.getVaultById(route.params.vaultId)
    }
    catch (error) {
        Pop.error(error);
        logger.error("Could not get this album", error)
        if (error instanceof AxiosError && error.response.data.includes('Invalid id')) {
            router.push({ name: 'Home' })
        }
    }
}

async function getKeepsByVaultId() {
    try {
        vaultsService.getKeepsByVaultId(route.params.vaultId)
    }
    catch (error) {
        Pop.error(error);
        logger.error(error)
    }
}

async function deleteVault(vaultId) {
    if (await Pop.confirm('Are you sure you want to delete this vault?')) {
        vaultsService.deleteVault(vaultId)
        router.push({ name: 'Home' })
    }
}
</script>


<template>
    <div class="d-flex justify-content-center" v-if="vault">
        <!-- <div class="position-relative">
            <img :src="vault.img" alt="" class="vault-img rounded mt-4 shadow">
            <div class="weird-things-are-happening fs-1">
                <div>{{ vault.name }}</div>
                <div class="fs-5 ms-5">by: {{ vault.creator.name }}</div>
            </div>
        </div> -->

        <div class="vault-img rounded mt-3 d-flex align-items-end justify-content-center"
            :style="`--bg-url: url(${vault.img})`">
            <div class="fs-3 weird-things-were-happening text-center mb-3">
                <div>{{ vault.name }}</div>
                <div class="fs-5 text-lowercase">by: {{ vault.creator.name }}</div>
            </div>
        </div>
    </div>
    <div v-if="keeps" class="text-center my-3 container">
        <div class="btn btn-lavender rounded-pill">
            <div v-if="keeps.length == 1">1 Keep</div>
            <div v-else>{{ keeps.length }} Keeps</div>
        </div>
        <div class="text-end" v-if="vault?.creatorId == account?.id">
            <div @click="deleteVault(vault?.id)" class="btn btn-danger rounded-pill">Delete Vault</div>
        </div>
        <div class="row mt-5">
            <div class="col-4" v-for="keep in keeps" :key="'keep-id-' + keep.id" :title="'take a look at ' + keep.name">
                <KeepCard :keep="keep" />
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.vault-img {
    width: 40vw;
    height: 20vh;
    object-fit: cover;
    background-image: var(--bg-url);
}

// .weird-things-are-happening {
//     position: absolute;
//     bottom: 0;
//     left: 25%;
//     text-shadow: 1px 1px 3px #000000;
//     color: white;
// }
.weird-things-were-happening {
    text-shadow: 1px 1px 3px #000000;
    color: white;
}
</style>