mergeInto(LibraryManager.library, {
  autologin: function(){
    autoLogin();
  },
  login: function(type) {
    selectWallet(UTF8ToString(type));
  },
  getAssetData: function() {
    getAssetD();
  },
  getNinjaData: function() {
    getNinjaD();
  },
  searchcz: function(int) {
     var asset_id  = UTF8ToString(int); 
     search_citizen(asset_id);                                        
  },
  registernft: function(int){
    var id  = UTF8ToString(int); 
    registerAsset(id);
  },
  mintcitizens: function(){
    mintcitizens();
  },
  burncitizennft: function(){
    burncitizennft();
  },
});
