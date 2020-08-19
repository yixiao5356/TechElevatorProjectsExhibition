import axios from 'axios';

export default {

  getQuickSelectButtonTopics(){
    return axios.get('/QuickSelect');
  },


}