define('vm.viewmedica.display',
    ['ko', 'config', 'jquery'],
    function(ko, config, $) {


        function ViewMedicaDisplay() {
            this.animationTag = ko.observable();
        }

        ViewMedicaDisplay.prototype.init = function(animationCode) {
            if (animationCode !== null) {
                this.animationTag(animationCode);
                //hate this shit... reloads there player every time
                openthis = animationCode;
                vm_open();
            } else {
                $('.viewmedica-container').empty();
            }
        };

        return ViewMedicaDisplay;

    });