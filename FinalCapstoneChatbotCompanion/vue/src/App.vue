<!-- Main Application Template. 
     Note that you have classes from bootstrap available to you.
     See https://getbootstrap.com/docs/4.0/components/alerts/ for reference on bootstrap -->
<template>
  <div id="app">
    <div class="container" id="grid-styling">
      <!-- <div id="nav" class="navbar navbar-expand-lg">
        <router-link v-bind:to="{ name: 'home' }">
          <img id="mainicon" src="../public/techelevatormain.png" alt="techelevator logo" />
        </router-link>
        <div id="navtag">
          <router-link class="navitem" v-bind:to="{ name: 'home' }">
            <i class="fas fa-home"></i>
            This is a font awesome icon
            Home
          </router-link>
          <router-link
            class="navitem"
            v-bind:to="{ name: 'register' }"
            v-if="!$store.state.token"
          >&nbsp;&nbsp;Register</router-link>
          <router-link
            class="navitem"
            v-bind:to="{ name: 'admin' }"
            v-if="$store.state.user.role === 'admin'"
          >&nbsp;&nbsp;Admin</router-link>
          <router-link
            class="navitem"
            v-bind:to="{ name: 'login' }"
            v-if="!$store.state.token"
          >&nbsp;&nbsp;Login</router-link>
          <router-link
            class="navitem"
            v-bind:to="{ name: 'logout' }"
            v-if="$store.state.token"
          >&nbsp;&nbsp;Logout</router-link>
          <span class="themeselector">
            <span class="dropbtn">Change Theme</span>
            <div class="dropdown-content">
              <a href="#" v-on:click="changeLight()">Light Theme</a>
              <a href="#" v-on:click="changeDark()">Dark Theme</a>
              <a href="#" v-on:click="changeBlue()">Blue Theme</a>
              <a href="#" v-on:click="changePurple()">Purple Theme</a>
              <a href="#" v-on:click="changeCobalt()">Cobalt Theme</a>
              <a href="#" v-on:click="changeNoir()">Noir Theme</a>
              <a href="#" v-on:click="changeRainbow()">Rainbow Theme</a>
            </div>
          </span>
        </div>
      </div> -->
      <nav id="nav" class="navbar navbar-expand-sm">
        <router-link tag="li" class="navitem navbar-brand" v-bind:to="{ name: 'home' }">
          <img v-if="purpleTheme" src="../public/TElogodark.png" alt="techelevator logo" id="darkicon" />
            <img v-else src="../public/techelevatormain.png" alt="techelevator logo" id="mainicon"  />
          </router-link>
        <button
          class="navbar-toggler"
          type="button"
          data-toggle="collapse"
          data-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <i class="fas fa-bars"></i>
        </button>

        <div class="collapse navbar-collapse moveThisAround" id="navbarSupportedContent">
          <ul class="navbar-nav">
          <!--<router-link tag="li" 
            class="navitem nav-item"
            v-bind:to="{ name: 'register' }"
            v-if="!$store.state.token"
          >&nbsp;&nbsp;Register</router-link> -->
          <router-link tag="li" 
            class="navitem nav-item"
            v-bind:to="{ name: 'admin' }"
            v-if="$store.state.user.role === 'admin'"
          >&nbsp;&nbsp;Admin</router-link>
          <router-link tag="li" 
            class="navitem nav-item"
            v-bind:to="{ name: 'login' }"
            v-if="!$store.state.token"
          >&nbsp;&nbsp;Login as Admin</router-link>
          <router-link tag="li" 
            class="navitem nav-item"
            v-bind:to="{ name: 'logout' }"
            v-if="$store.state.token"
          >&nbsp;&nbsp;Logout</router-link>
            <li class=" navitem nav-item dropdown">
              <a
                class="navitem dropdown-toggle"
                href="#"
                id="navbarDropdown"
                role="button"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
              >&nbsp;&nbsp;Change Themes</a>
              <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                <a href="#" class="dropdown-item" v-bind:class="{activeTheme: lightTheme}" v-on:click="changeLight()">Light Theme</a>
                <a href="#" class="dropdown-item" v-bind:class="{activeTheme: darkTheme}" v-on:click="changeDark()">Dark Theme</a>
                <a href="#" class="dropdown-item" v-bind:class="{activeTheme: blueTheme}" v-on:click="changeBlue()">Blue Theme</a>
                <a href="#" class="dropdown-item" v-bind:class="{activeTheme: purpleTheme}" v-on:click="changePurple()">Purple Theme</a>
                <a href="#" class="dropdown-item" v-bind:class="{activeTheme: cobaltTheme}" v-on:click="changeCobalt()">Cobalt Theme</a>
                <a href="#" class="dropdown-item" v-bind:class="{activeTheme: rainbowTheme}" v-on:click="changeRainbow()">Rainbow Theme</a>
              </div>
            </li>
          </ul>
        </div>
      </nav>
      <router-view id="mainview" class="mb-4" />
      <div class="footer">
        <footer>This application was developed by Cohort[11].NET.MIKE</footer>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      theme: 'light'
    };
  },

  computed: {
    blueTheme() {
      if (this.theme === "blue") {
      return true;
      }
      else{
        return false;
      }
    },
    rainbowTheme() {
      if (this.theme === "rainbow") {
        return true;
      }
      else{
        return false;
      }
    },
    purpleTheme() {
      if (this.theme === "purple") {
        return true;
      }
      else{
        return false;
      }
    },
    cobaltTheme() {
      if (this.theme === "cobalt") {
        return true;
      }
      else{
        return false;
      }
    },
    darkTheme() {
      if (this.theme === "dark") {
        return true;
      }
      else{
        return false;
      }
    },
    lightTheme() {
      if (this.theme === "light") {
        return true;
      }
      else{
        return false;
      }
    }
  },
  created() {
    const theme = localStorage.getItem("theme");
    if (theme === "blue")
    {
      this.changeBlue();
    }
    else if (theme === "rainbow")
    {
      this.changeRainbow();
    }
    else if (theme === "purple")
    {
      this.changePurple();
    }
    else if (theme === "dark")
    {
      this.changeDark();
    }
    else if (theme === "cobalt")
    {
      this.changeCobalt();
    }
    else{
      this.changeLight();
    }
  },

  methods: {
    changeLight() {
      const root = document.documentElement;
      root.style.setProperty("--color-primary", "white");
      root.style.setProperty("--color-secondary", "#999999");
      root.style.setProperty("--color-ternary", "#3B3E42");
      root.style.setProperty("--color-accent", "#00ADEE");
      root.style.setProperty("--font-color", "black");
      root.style.setProperty("--bot-font-color", "black");
      root.style.setProperty("--color-botmessage", "white");
      root.style.setProperty("--color-usermessage", "#999999");
      root.style.setProperty("--card-color", "rgb(175, 174, 174)");
      this.theme = "light"
      localStorage.setItem("theme", this.theme);
      
    },
    changeBlue() {
      const root = document.documentElement;
      root.style.setProperty("--color-primary", "#1d3557");
      root.style.setProperty("--color-secondary", "#457b9d");
      root.style.setProperty("--color-ternary", "#e63946");
      root.style.setProperty("--color-accent", "#e63946");
      root.style.setProperty("--font-color", "#f1faee");
      root.style.setProperty("--bot-font-color", "white");
      root.style.setProperty("--color-botmessage", "black");
      root.style.setProperty("--color-usermessage", "#999999");
      root.style.setProperty("--card-color", "#f1faee");
      this.theme = "blue"
      localStorage.setItem("theme", this.theme);
    },
    changeRainbow() {
      const root = document.documentElement;
      root.style.setProperty("--color-primary", "violet");
      root.style.setProperty("--color-secondary", "red");
      root.style.setProperty("--color-ternary", "yellow");
      root.style.setProperty("--color-accent", "green");
      root.style.setProperty("--font-color", "blue");
      root.style.setProperty("--bot-font-color", "indigo");
      root.style.setProperty("--color-botmessage", "orange");
      root.style.setProperty("--color-usermessage", "red");
      root.style.setProperty("--card-color", "green");
      this.theme = "rainbow"
      localStorage.setItem("theme", this.theme);
    },
    changePurple() { /*changed this color scheme because John said it was "truly awful"*/
      const root = document.documentElement;
      root.style.setProperty("--color-primary", "#363E8B");
      root.style.setProperty("--color-secondary", "black");
      root.style.setProperty("--color-ternary", "yellow");
      root.style.setProperty("--color-accent", "black");
      root.style.setProperty("--font-color", "yellow");
      root.style.setProperty("--bot-font-color", "Black");
      root.style.setProperty("--color-botmessage", "yellow");
      root.style.setProperty("--color-usermessage", "yellow");
      root.style.setProperty("--card-color", "black");
      this.theme = "purple"
      localStorage.setItem("theme", this.theme);
    },
    changeDark() {
      const root = document.documentElement;
      root.style.setProperty("--color-primary", "#242626");
      root.style.setProperty("--color-secondary", "black"); //this was #FFFFFF
      root.style.setProperty("--color-ternary", "#454A4D");
      root.style.setProperty("--color-accent", "#B4BBBF");
      root.style.setProperty("--font-color", "#FFFFFF");
      root.style.setProperty("--bot-font-color", "#FFFFFF");
      root.style.setProperty("--color-botmessage", "#242626");
      root.style.setProperty("--color-usermessage", "#B4BBBF");
      root.style.setProperty("--card-color", "#B4BBBF");
      this.theme = "dark"
      localStorage.setItem("theme", this.theme);
    },
    changeCobalt() {
      const root = document.documentElement;
      root.style.setProperty("--color-primary", "#051431");
      root.style.setProperty("--color-secondary", "#426fe1");
      root.style.setProperty("--color-ternary", "#5c8cec");
      root.style.setProperty("--color-accent", "#4c5979");
      root.style.setProperty("--font-color", "white");
      root.style.setProperty("--bot-font-color", "white");
      root.style.setProperty("--color-botmessage", "#2c3454");
      root.style.setProperty("--color-usermessage", "#426fe1");
      root.style.setProperty("--card-color", "#4c5979");
      this.theme = "cobalt"
      localStorage.setItem("theme", this.theme);
    },
  },
};
</script>

<!-- Application-Wide Styles go here -->
<style>
#grid-styling {
  display: grid;
  grid-template-areas:
    "navbar"
    "mainbody"
    "footer";
}
.dropdown-toggle:hover{
  color: var(--color-accent);
  text-decoration: none;
  cursor: pointer;
}

.themeselector:hover .dropdown-content {
  display: block;
}

.activeTheme {
  font-weight: 900;
}

.fa-bars {
  color: var(--color-accent)
}

.btn-primary {
  background-color: var(--font-color);
  color: var(--color-primary)
}

.btn-primary:hover {
  background-color: var(--color-accent);
  color: var(--font-color);
}

.navbar {
  padding: 0px;
}

.dropdown-item{
  color: var(--font-color);
}

.dropdown-item:hover {
  background-color: var(--color-accent);
  color: white;
}

.themeselector {
  position: relative;
  display: inline-block;
  font-family: proxima-nova, sans-serif;
  font-style: normal;
  font-size: large;
}

.dropbtn:hover {
  color: var(--color-accent);
}
.dropdown-menu{
  background-color: var(--color-primary);
  box-shadow: 0px 8px 16px 0px var(--font-color);
}

.dropbtn {
  color: var(--font-color);
  padding: 16px;
  font-size: 16px;
  border: none;
  font-family: proxima-nova, sans-serif;
  font-style: normal;
  font-size: large;
}

.dropdown-content a {
  color: var(--font-color);
  padding: 12px 16px;
  text-decoration: none;
  display: block;
}

.dropdown-content {
  display: none;
  position: absolute;
  background-color: var(--color-primary);
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px var(--font-color);
  z-index: 1;
}

.footer {
  color: var(--font-color);
  grid-area: "footer";
}

:root {
  --color-primary: white;
  --color-secondary: #999999;
  --color-ternary: #3b3e42;
  --color-accent: #00adee;
  --font-color: black;
  --color-botmessage: white;
  --color-usermessage: #999999;
  --bot-font-color: black;
  --card-color: rgb(175, 174, 174);
}

body {
  background-color: var(--color-primary);
}

.moveThisAround {
  justify-content: flex-end;
}

#nav {
  display: flex;
  flex-direction: row;
  justify-content: space-around;
  align-items: center;
  background-color: var(--color-primary);
  grid-area: "navbar";
}
.navbar-nav {
  align-items: center;
  margin-inline-end: 0px;
}
#mainicon {
  height: 50px;
}
#darkicon {
  height: 50px;
}
.navitem {
  color: var(--font-color);
  text-align: center;
  text-decoration: none;
  text-decoration-line: none;
}
#navtag {
  font-family: proxima-nova, sans-serif;
  font-style: normal;
  font-size: large;
}
li:hover {
  color: var(--color-accent);
  text-decoration: none;
  text-decoration-line: none;
  text-decoration-style: initial;
  text-decoration-color: initial;
  cursor: pointer;
}



.nav-link{
  padding: 0px;
}

@media screen and (max-width: 425px) {
  #navtag {
    display: flex;
    flex-direction: column;
    align-items: center;
  }
}

#mainview {
  grid-area: "mainbody";
}
</style>