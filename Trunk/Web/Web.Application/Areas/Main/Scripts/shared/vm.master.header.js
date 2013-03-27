define('vm.master.header',
['ko', 'config', 'model.user', 'kb', 'vm.login.dialog','jquery'],
    function (ko, config, user, kb, login, $) {

        var userId = $('#userid').val(),
        emailAddress = $('#userEmail').val(),

        user2 = new user({ id: 2 }),
        firstName = kb.observable(user2, 'firstName'),
        lastName = kb.observable(user2, 'lastName');
        

        var isAuthenticated = ko.observable(false);

        if (userId > 0)
            isAuthenticated(true);


        //var name = ko.computed(function() {
        //    return firstName() + ' ' + lastName();
        //});

        var name = ko.observable(emailAddress);

        $('#header-nav').removeClass('hidden');
        
        return {
            name: name,
            isAuthenticated: isAuthenticated,
            toggleSignUp: login.toggleSignUp
        };
    });