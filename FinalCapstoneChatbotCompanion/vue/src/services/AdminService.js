import axios from 'axios';

export default {

  addToDatabase(databaseItem){
    const headers = {
        'Accept': 'application/json',
        'Content-Type': 'application/json',};
    return axios.post('/Admin/add', databaseItem, {headers:headers});
  },

  getAllDatabaseEntries(){
    return axios.get('/Admin');
  },

  deleteEntry(databaseItem){
    
    return axios.delete('/Admin/delete/'+ databaseItem.categoryName + '/' + databaseItem.id);
  },

  updateEntry(databaseItem){
    return axios.put('/Admin/update',databaseItem);
  },

  getAllUserRequests(){
    return axios.get('/Admin/requestrecord');
  },

  

}
