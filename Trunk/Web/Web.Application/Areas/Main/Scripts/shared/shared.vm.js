define('shared.vm',
['vm.master.header','vm.master.footer'],
    function (header, footer) {
        return {
            header: header,
            footer: footer
        };
    });