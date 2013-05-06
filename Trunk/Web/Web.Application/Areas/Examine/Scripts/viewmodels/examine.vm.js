define('examine.vm',
['vm.examine.container', 'vm.examine.skeleton', 'vm.examine.detail'],
    function (container, skeleton, detail) {

        var viewIds = {
            skeleton: '#examine-skeleton',
            container: '#examine-container',
            detail: '#examine-detail'
        };

        return {
            container: container,
            skeleton: skeleton,
            detail: detail,
            viewIds : viewIds
        };
    });