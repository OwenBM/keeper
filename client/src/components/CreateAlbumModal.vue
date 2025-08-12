<script setup>
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Modal } from 'bootstrap';
import { ref } from 'vue';

const albumData = ref({
    name: "",
    img: "",
    description: "",
    isPrivate: false
})

async function createAlbum() {
    logger.log(albumData)
    vaultsService.createAlbum(albumData.value)
    clearForm()
    Modal.getOrCreateInstance('#CreateAlbumModal').hide();
}

function clearForm() {
    albumData.value = {
        name: "",
        img: "",
        description: "",
        isPrivate: null
    }
}
</script>


<template>
    <div class="modal fade" id="CreateAlbumModal">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body">
                    <h2 class="mb-4 text-center">Create Album</h2>
                    <form @submit.prevent="createAlbum()">
                        <div class="row">
                            <div class="col-6">
                                <label for="form-name">Name</label>
                                <input v-model="albumData.name" required minlength="3" type="text" class="form-control"
                                    id="form-name">
                            </div>
                            <div class="col-6">
                                <label for="form-img">Image Url</label>
                                <input v-model="albumData.img" type="Url" required class="form-control" id="form-img">
                            </div>
                            <div>
                                <label for="form-desc">Description</label>
                                <input v-model="albumData.description" type="text" required minlength="15"
                                    class="form-control" id="form-desc">
                            </div>
                            <div class="d-flex justify-content-between align-items-center mt-4">
                                <div class="mb-3 form-check col-4">
                                    <input v-model="albumData.isPrivate" type="checkbox" class="form-check-input"
                                        id="form-private">
                                    <label class="form-check-label" for="form-private">Make this private?</label>
                                </div>
                                <button type="submit" class="btn btn-primary text-white col-3">Submit <span
                                        class="mdi mdi-plus"></span>
                                </button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped></style>