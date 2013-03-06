define('vm.master.header',
['ko', 'config'],
    function (ko, config) {

        var user = ko.observable();
        var isAuthenticated = ko.observable(false);
        user('KRB');

        return {
            user: user,
            isAuthenticated : isAuthenticated
        };
    });