
<template>
  <div class="container">
    <div class="btn-group btn-group-toggle" id="buttonblock">
      
    <label class="btn btn-primary" v-bind:class='{highline: ActiveAdd() }'><input type="radio"  v-model="DisplayAdd" value="DisplayAdd" />Add</label>
    <label class="btn btn-primary" v-bind:class='{highline: ActiveUpdateAndDelete() }'><input type="radio"   v-model="DisplayAdd" value="DisplayUpdateOrDelete" />Modify/Delete</label>
    <label class="btn btn-primary" v-bind:class='{highline: ActiveUserRequest() }'><input type="radio"   v-model="DisplayAdd" value="DisplayUserRequests" />User Requests</label>
    
    </div>
    


    
    <AdminAdd v-if="DisplayAdd == 'DisplayAdd'" />
    <AdminUpdateOrDelete v-else-if= "DisplayAdd == 'DisplayUpdateOrDelete'" />
    <AdminUserRequests v-else-if="DisplayAdd == 'DisplayUserRequests'" />

  </div>
</template>

<script>
import AdminAdd from "../components/AdminAdd.vue";
import AdminUpdateOrDelete from "../components/AdminUpdateOrDelete.vue";
import AdminUserRequests from "../components/AdminUserRequests.vue"

export default {
  methods: {
    /*ToggleAdd() {
      this.DisplayAdd = !this.DisplayAdd;
      this.DisplayUpdateOrDelete = false;
    },

    ToggleUpdateOrDelete() {
      this.DisplayUpdateOrDelete = !this.DisplayUpdateOrDelete;
      this.DisplayAdd = false;
    },*/
    ActiveAdd(){
      if(this.DisplayAdd == "DisplayAdd"){
        return true;
      }
      else{
        return false;
      }
    },
    ActiveUpdateAndDelete(){
      if(this.DisplayAdd == "DisplayUpdateOrDelete"){
        return true;
      }
      else{
        return false;
      }
    },
    ActiveUserRequest(){
      if(this.DisplayAdd == "DisplayUserRequests"){
        return true;
      }
      else{
        return false;
      }
    }

  },

  data() {
    return {
      DisplayAdd: "DisplayAdd",

    };
  },

  components: {
    AdminAdd,
    AdminUpdateOrDelete,
    AdminUserRequests
  },
  computed:{
    
  }
};
</script>


<style>


#adminActions {
  display: flex;
  justify-content: center;
}

li{
  color:var(--font-color)
}

/*
pointer-events: none;
*/

.adminbtn {
  box-shadow: 2px 2px 2px 2px var(--color-secondary);
  background: var(--color-primary);
  color: var(--font-color);
}

#buttonblock{
  display: flex;
  justify-content: space-around;
}

.highline{
  background-color: var(--color-secondary); /*this was TE blue*/
  color: white;
}

label{
  color:var(--font-color)
}

</style>