define('examine.vm',
['vm.examine.container', 'vm.examine.skeleton', 'vm.examine.detail', 'vm.examine.report'],
    function (container, skeleton, detail, report) {

        var viewIds = {
            skeleton: '#examine-skeleton',
            container: '#examine-container',
            detail: '#examine-detail',
            report: '#examine-report'
        };

        return {
            container: container,
            skeleton: skeleton,
            detail: detail,
            report: report,
            viewIds : viewIds
        };
    });