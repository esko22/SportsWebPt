define('vm.master.header',
['ko', 'config', 'model.user', 'kb'],
    function (ko, config, user, kb) {

        var user2 = new user({ id: 2 }),
        firstName = kb.observable(user2, 'firstName'),
        lastName = kb.observable(user2, 'lastName');

        user2.fetch({
            //success: function() {
            //    firstName(user2.get('firstName'));
            //    lastName(user2.get('lastName'));
            //}
        });
        
        var isAuthenticated = ko.observable(false);
        var name = ko.computed(function() {
            return firstName() + ' ' + lastName();
        });

        return {
            name: name,
            isAuthenticated : isAuthenticated
        };
    });