<template>
  <select class="add" v-on:change ="updateCategory()" v-model.number="selectedCatgory">
            <option value = "0"></option>
            <option value = "1">Pathway</option>
            <option value = "2">Curriculum</option>
            <option value = "4">Job</option>
            <option value = "3">Motivational Quote</option>
        </select>
</template>

<script>
export default {
    data() {
        return {
            selectedCatgory: 0,
        }
        
    },
    methods: {
        updateCategory() {
            this.$store.commit("SET_SELECTEDCATEGORY",this.selectedCatgory);
        }
    }
}
</script>

<style>

</style>