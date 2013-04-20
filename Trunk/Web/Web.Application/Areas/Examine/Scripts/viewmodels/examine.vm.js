define('examine.vm',
['vm.examine.container', 'vm.examine.skeleton', 'vm.examine.discomfort'],
    function (container, skeleton, discomfort) {

        var viewIds = {
            skeleton: '#skeleton-container',
            container: '#examine-container',
            discomfort: '#discomfort-detail'
        };

        return {
            container: container,
            skeleton: skeleton,
            discomfort: discomfort,
            viewIds : viewIds
        };
    });