import axios from 'axios';

export default {

  login(user) {
    return axios.post('/login', user);
  },

  register(user) {
    return axios.post('/login/register', user);
  },
  response(text){
    const headers = {
      'Content-Type': 'application/json',}
    return axios.post('/response', text, {headers:headers});
  },

}
