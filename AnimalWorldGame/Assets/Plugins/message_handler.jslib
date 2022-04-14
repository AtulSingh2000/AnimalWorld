mergeInto(LibraryManager.library, {
  autologin: function(){
    autoLogin();
  },
  login: function(type) {
    selectWallet(UTF8ToString(type));
  },
  logout: function() {
    logout();
  },
    getdmodata: function() {
    getdmodata();
  },
    getshopdata: function() {
    getshopdata();
  },
     getburnids: function() {
    getburnids();
  },
      getuserB: function() {
    getuserB();
  },
  depositawc: function(amount) {
    depositawc(UTF8ToString(amount));
  },
  withdrawawc: function(amount) {
    withdrawawc(UTF8ToString(amount));
  },
  filldmo: function(id,amount) {
    filldmo(UTF8ToString(id),UTF8ToString(amount));
  },
  buyshopl: function(id,amount) {
    buyshopl(UTF8ToString(id),UTF8ToString(amount));
  },
  burnid: function(id) {
    burnid(UTF8ToString(id));
  },
    boost: function(id,type){
    use_boost(UTF8ToString(id),UTF8ToString(type))
  },
  register_asset: function(r_asset_id,r_name,r_landid,r_type){
    register_nft(UTF8ToString(r_asset_id),UTF8ToString(r_name),UTF8ToString(r_landid),UTF8ToString(r_type));
  },
  deregister_asset: function(d_asset_id,d_name,d_type){
    deregister_nft(UTF8ToString(d_asset_id),UTF8ToString(d_name),UTF8ToString(d_type));
  },
  machine_start: function(m_asset_id,m_recipe_id,start_type){
    start_machine(UTF8ToString(m_asset_id),UTF8ToString(m_recipe_id),UTF8ToString(start_type));
  },
  machine_claim: function(mclaim_asset_id,mclaim_recipe_id,type){
    claim_machine(UTF8ToString(mclaim_asset_id),UTF8ToString(mclaim_recipe_id),UTF8ToString(type));
  },
  tree_claim: function(t_claimsymbol){
    claim_tree(UTF8ToString(t_claimsymbol));
  },
  claim_all_assets: function(type,subtype,land){
    claim_all_assets(UTF8ToString(type),UTF8ToString(subtype),UTF8ToString(land));
  },
  level_up_trx: function(asset_id,type){
    levelup(UTF8ToString(asset_id),UTF8ToString(type));
  },
  animal_claim: function(asset_ids){
    claim_animal(UTF8ToString(asset_ids));
  }
});
