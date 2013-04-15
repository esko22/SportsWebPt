define('model.user', ['backbone','config'],
    function(backbone, config) {
       var
           user = backbone.Model.extend({
               urlRoot: config.users,
               defaults : {
               }
        });

       return user;

    });