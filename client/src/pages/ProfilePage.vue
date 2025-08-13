<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import VaultCard from '@/components/VaultCard.vue';
import { accountService } from '@/services/AccountService.js';
import { profileService } from '@/services/ProfileService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted, ref } from 'vue';
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

const profileData = ref({
    name: null,
    picture: null,
    coverImg: null,
})

async function getProfilebyId() {
    try {
        await profileService.getProfilebyId(route.params.profileId)
    }
    catch (error) {
        Pop.error(error);
    }
}

async function getKeepsByProfileId() {
    try {
        await profileService.getKeepsByProfileId(route.params.profileId)
    }
    catch (error) {
        Pop.error(error);
    }
}

async function getVaultsByProfileId() {
    try {
        await profileService.getVaultsByProfileId(route.params.profileId)
    }
    catch (error) {
        Pop.error(error);
    }
}

async function editProfile() {
    try {
        await accountService.editAccount(profileData.value)
        logger.log(profileData.value)
    }
    catch (error) {
        Pop.error(error);
    }
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
            <div class="btn btn-primary mt-2" data-bs-toggle="modal" data-bs-target="#EditProfileModal">Edit Account
            </div>
        </div>
        <div class="container">
            <div class="">
                <div class="fs-4 fw-bold">Vaults</div>
                <div class="row">
                    <div v-for="vault in vaults" :key="vault.id"
                        class="col-lg-3 col-md-6 d-flex justify-content-center">
                        <VaultCard :vault="vault" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="fs-4 fw-bold mt-4">Keeps</div>
                <div class="masonry" v-if="keeps.length">
                    <div v-for="keep in keeps" :key="'keep-id-' + keep.id" class="">
                        <KeepCard :keep="keep" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="fs-1" v-else>
        <div>Loading... <span class="mdi mdi-loading mdi-spin"></span></div>
    </div>

    <div class="modal fade" id="EditProfileModal" aria-hidden="true" v-if="profile">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Edit Profile</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="fs-3 text-center">Edit Profile</div>
                    <form>
                        <label for="form-name">Name</label>
                        <input id="form-name" v-model="profileData.name" class="form-control"
                            :placeholder="profile.name" type="text">
                        <div class="my-2">
                            <label for="form-picture">Picture</label>
                            <input id="form-picture" type="text" v-model="profileData.picture"
                                :placeholder="profile.picture" class="form-control">
                        </div>
                        <label for="form-coverImg">Cover Image</label>
                        <input id="form-coverImg" type="text" v-model="profileData.coverImg"
                            :placeholder="profile.coverImg" class="form-control">

                        <!-- <label for="form-name">Name</label>
                        <input v-model="albumData.name" required minlength="3" type="text" class="form-control"
                            id="form-name"> -->
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary" @click="editProfile()">Save changes</button>
                </div>
            </div>
        </div>
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

.masonry {
    columns: 200px;
    column-gap: 1rem;
}
</style>