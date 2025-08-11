<script setup>
import { AppState } from '@/AppState.js';
import { computed } from 'vue';



const keep = computed(() => AppState.Keep)
</script>


<template>
    <div id="KeepDetailsModal" class="modal fade">
        <div class="modal-dialog modal-size">
            <div class="modal-content">
                <div class="modal-body p-0">
                    <div>
                        <div class="row">
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
                                        <div>dropdown</div>
                                        <div class="d-flex align-items-center mb-2">
                                            <img class="profile-picture me-1" :src="keep.creator.picture" alt="">
                                            <div>{{ keep.creator.name }}</div>
                                        </div>
                                    </div>
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
</style>