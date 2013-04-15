define('examine.vm',
['vm.examine.master', 'vm.examine.skeletor'],
    function (master, skeletor) {

        var viewIds = {
            skeletor: '#skeletor-container',
            master: ''
        };

        return {
            master: master,
            skeletor: skeletor,
            viewIds : viewIds
        };
    });