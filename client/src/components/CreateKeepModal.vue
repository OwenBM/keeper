<script setup>
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Modal } from 'bootstrap';
import { ref } from 'vue';


const keepData = ref({
    name: "",
    description: "",
    img: ""

})

async function createKeep() {
    logger.log(keepData.value)
    keepsService.createKeep(keepData.value)
    clearForm()
    Modal.getOrCreateInstance('#CreateKeepModal').hide();
}

function clearForm() {
    keepData.value = {
        name: "",
        img: "",
        description: "",
    }
}

</script>


<template>
    <div class="modal fade" id="CreateKeepModal">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body">
                    <h2 class="mb-4 text-center">Create Keep</h2>
                    <form @submit.prevent="createKeep()">
                        <div class="row">
                            <div class="col-6">
                                <label for="form-name">Name</label>
                                <input v-model="keepData.name" required minlength="3" type="text" class="form-control"
                                    id="form-name">
                            </div>
                            <div class="col-6">
                                <label for="form-img">Image Url</label>
                                <input v-model="keepData.img" type="Url" required class="form-control" id="form-img">
                            </div>
                            <div>
                                <label for="form-desc">Description</label>
                                <input v-model="keepData.description" type="text" required minlength="15"
                                    class="form-control" id="form-desc">
                            </div>
                            <div class="text-end mt-3">
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