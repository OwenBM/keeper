<script setup>
import { Vault } from '@/models/Vault.js';



defineProps({
    vault: { type: Vault, required: true }
})
</script>


<template>
    <RouterLink :to="{ name: 'Vault', params: { vaultId: vault.id } }">
        <div class="position-relative mb-3 shadow hover-craft">
            <img :src="vault.img" alt="broken image..." class="vault-bg rounded">
            <div class="text-uppercase text-white vault-name m-3 fs-5">{{ vault.name }}</div>
            <div v-if="vault.isPrivate" class="private-icon px-1 m-2" title="private"><span class="mdi mdi-lock"></span>
            </div>
        </div>
    </RouterLink>
</template>


<style lang="scss" scoped>
.vault-bg {
    height: 20vh;
    width: 200px;
    object-fit: cover;
}

.vault-name {
    text-shadow: 1px 1px 3px #000000;
    position: absolute;
    bottom: 0;
}

.private-icon {
    position: absolute;
    bottom: 0;
    right: 0;
    background-color: white;
    border-radius: 50%;
}
</style>