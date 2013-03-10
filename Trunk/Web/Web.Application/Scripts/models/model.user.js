define('model.user', ['backbone'],
    function(backbone) {
       var
           user = backbone.Model.extend({
               urlRoot: 'http://localhost:8022/users',
               defaults : {
                   firstName: 'alex',
                   lastName: 'nut',
               }
        });

       return user;

    });