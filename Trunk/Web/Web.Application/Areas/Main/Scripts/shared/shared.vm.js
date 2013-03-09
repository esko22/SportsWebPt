define('shared.vm',
['vm.master.header', 'vm.master.footer', 'vm.login.dialog'],
    function (header, footer, login) {
        return {
            header: header,
            footer: footer,
            login: login
        };
    });