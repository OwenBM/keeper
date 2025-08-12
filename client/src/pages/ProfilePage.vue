<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import VaultCard from '@/components/VaultCard.vue';
import { profileService } from '@/services/ProfileService.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();

const profile = computed(() => AppState.profile)
const keeps = computed(() => AppState.Keeps)
const vaults = computed(() => AppState.vaults)

onMounted(() => {
    getProfilebyId();
    getKeepsByProfileId();
    getVaultsByProfileId();
})

async function getProfilebyId() {
    profileService.getProfilebyId(route.params.profileId)
}

async function getKeepsByProfileId() {
    profileService.getKeepsByProfileId(route.params.profileId)
}

async function getVaultsByProfileId() {
    profileService.getVaultsByProfileId(route.params.profileId)
}
</script>


<template>
    <div v-if="AppState.profile">
        <div class="d-flex justify-content-center mt-5">
            <div class="cover-image rounded" :style="`--bg-url: url(${profile.coverImg})`">
            </div>
        </div>
        <div class="text-center cover-name">
            <img class="profile-picture-large" :src="profile.picture" alt="folly">
            <div class="fs-3 fw-bold">{{ profile.name }}</div>
            <div>{{ vaults?.length }} Vaults | {{ keeps?.length }} Keeps</div>
            <div class="btn btn-primary mt-2">Edit Account</div>
        </div>
        <div class="container">
            <div class="row">
                <div class="fs-4 fw-bold">Vaults</div>
                <div v-for="vault in vaults" :key="vault.id"
                    class="col-lg-3 col-md-4 col-6 d-flex justify-content-center">
                    <VaultCard :vault="vault" />
                </div>
            </div>
            <div class="row">
                <div class="fs-4 fw-bold mt-4">Keeps</div>
                <div class="row" v-if="keeps.length">
                    <div v-for="keep in keeps" :key="'keep-id-' + keep.id" class="col-3">
                        <KeepCard :keep="keep" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="fs-1" v-else>
        <div>Loading... <span class="mdi mdi-loading mdi-spin"></span></div>
    </div>
</template>


<style lang="scss" scoped>
.cover-image {
    height: 40dvh;
    width: 60%;
    object-fit: contain;
    background-image: var(--bg-url);
    background-size: cover;
    background-position: center;
}

.cover-name {
    margin-top: -50px;
}

.profile-picture-large {
    height: 90px;
    width: auto;
    aspect-ratio: 1/1;
    border-radius: 50%;
    border: 3px;
    border-style: groove;
}
</style>