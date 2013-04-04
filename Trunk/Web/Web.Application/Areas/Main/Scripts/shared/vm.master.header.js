define('vm.master.header',
['ko', 'config', 'model.user', 'kb', 'vm.login.dialog','jquery'],
    function (ko, config, user, kb, login, $) {

        //TODO: this needs to come from user passed in
        //ko mapping plugin fromJS
        var userId = $('#userid').val(),
        emailAddress = $('#userEmail').val(),
        isAuthenticated = ko.observable(false)

        if (userId > 0)
            isAuthenticated(true);


        var name = ko.observable(emailAddress);
        
        return {
            name: name,
            isAuthenticated: isAuthenticated,
            toggleSignUp: login.toggleSignUp
        };
    });