<template>
    <div>
        <div class="themeselector">
            <button class="dropbtn">Dropdown</button>
            <div class="dropdown-content">
                <a href="#">Light Theme</a>
                <a href="#">Dark Theme</a>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    
}
</script>

<style scoped>
.themeselector:hover .dropdown-content {display: block;}

.themeselector:hover .dropbtn {background-color:  #00ADEE;}


.themeselector {
  position: relative;
  display: inline-block;
}

.dropbtn {
  background-color: #999999;
  color: white;
  padding: 16px;
  font-size: 16px;
  border: none;
}

.dropdown-content {
  display: none;
  position: absolute;
  background-color: #f1f1f1;
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
  z-index: 1;
}
</style>