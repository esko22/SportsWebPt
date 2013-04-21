define('examine.vm',
['vm.examine.container', 'vm.examine.skeleton', 'vm.examine.description'],
    function (container, skeleton, description) {

        var viewIds = {
            skeleton: '#skeleton-container',
            container: '#examine-container',
            description: '#description-detail'
        };

        return {
            container: container,
            skeleton: skeleton,
            description: description,
            viewIds : viewIds
        };
    });