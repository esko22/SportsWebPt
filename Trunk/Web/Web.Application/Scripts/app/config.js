define('config',['ko'],
    function (ko) {
        
        var viewIds = {
            header: '#master-header',
            footer: '#master-footer',
            login: '#login-dialog'
        };

        var currentUser = ko.observable();
        
        return {
            viewIds: viewIds,
            currentUser: currentUser
        };

    });
