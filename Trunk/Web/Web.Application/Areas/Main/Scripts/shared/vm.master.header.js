define('vm.master.header',
['ko', 'config'],
    function (ko, config) {

        var user = ko.observable(); 

        user('KRB');

        return {
            user : user
        };
    });