/* Load this script using conditional IE comments if you need to support IE 7 and IE 6. */

window.onload = function() {
	function addIcon(el, entity) {
		var html = el.innerHTML;
		el.innerHTML = '<span style="font-family: \'SWPT\'">' + entity + '</span>' + html;
	}
	var icons = {
			'iconmoon-search' : '&#xe01d;',
			'iconmoon-google-plus' : '&#xf0d5;',
			'iconmoon-facebook-sign' : '&#xf082;',
			'iconmoon-twitter-sign' : '&#xf081;',
			'iconmoon-github-sign' : '&#xf092;',
			'iconmoon-camera-retro' : '&#xe01e;',
			'iconmoon-cogs' : '&#xf085;',
			'iconmoon-target' : '&#xe01f;',
			'iconmoon-target-2' : '&#xe020;',
			'iconmoon-magnifier' : '&#xe021;',
			'iconmoon-user-md' : '&#xf0f0;',
			'iconmoon-stethoscope' : '&#xf0f1;',
			'iconmoon-building' : '&#xf0f7;',
			'iconmoon-refresh' : '&#xf021;',
			'iconmoon-repeat' : '&#xf01e;',
			'iconmoon-facebook' : '&#xe022;',
			'iconmoon-twitter' : '&#xe023;',
			'iconmoon-google-plus-2' : '&#xe025;',
			'iconmoon-google-plus-3' : '&#xe026;',
			'iconmoon-google-plus-4' : '&#xe027;',
			'iconmoon-untitled' : '&#xe02b;',
			'iconmoon-untitled-2' : '&#xe02d;',
			'iconmoon-untitled-3' : '&#xe02e;',
			'iconmoon-untitled-4' : '&#xe02f;',
			'iconmoon-untitled-5' : '&#xe030;',
			'iconmoon-untitled-6' : '&#xe032;',
			'iconmoon-untitled-7' : '&#xe033;',
			'iconmoon-untitled-8' : '&#xe034;',
			'iconmoon-untitled-9' : '&#xe035;',
			'iconmoon-untitled-10' : '&#xe036;',
			'iconmoon-untitled-11' : '&#xe037;',
			'iconmoon-untitled-12' : '&#xe038;',
			'iconmoon-untitled-13' : '&#xe039;',
			'iconmoon-untitled-14' : '&#xe03a;',
			'iconmoon-untitled-15' : '&#xe03b;',
			'iconmoon-untitled-16' : '&#xe03c;',
			'iconmoon-untitled-17' : '&#xe03d;',
			'iconmoon-untitled-18' : '&#xe03e;',
			'iconmoon-untitled-19' : '&#xe03f;',
			'iconmoon-untitled-20' : '&#xe040;',
			'iconmoon-untitled-21' : '&#xe043;',
			'iconmoon-untitled-22' : '&#xe044;',
			'iconmoon-untitled-23' : '&#xe047;',
			'iconmoon-untitled-24' : '&#xe048;',
			'iconmoon-untitled-25' : '&#xe04a;',
			'iconmoon-untitled-26' : '&#xe04b;',
			'iconmoon-untitled-27' : '&#xe04c;',
			'iconmoon-untitled-28' : '&#xe04d;',
			'iconmoon-untitled-29' : '&#xe04e;',
			'iconmoon-untitled-30' : '&#xe04f;',
			'iconmoon-untitled-31' : '&#xe050;',
			'iconmoon-untitled-32' : '&#xe051;',
			'iconmoon-untitled-33' : '&#xe052;',
			'iconmoon-untitled-34' : '&#xe053;',
			'iconmoon-untitled-35' : '&#xe054;',
			'iconmoon-untitled-36' : '&#xe055;',
			'iconmoon-untitled-37' : '&#xe056;',
			'iconmoon-untitled-38' : '&#xe057;',
			'iconmoon-untitled-39' : '&#xe058;',
			'iconmoon-untitled-40' : '&#xe059;',
			'iconmoon-untitled-41' : '&#xe05a;',
			'iconmoon-locked' : '&#xe000;',
			'iconmoon-unlocked' : '&#xe05b;',
			'iconmoon-suitcase' : '&#xe05c;',
			'iconmoon-suitcase-2' : '&#xe05d;',
			'iconmoon-chat' : '&#xe05e;',
			'iconmoon-envelope' : '&#xe05f;',
			'iconmoon-download' : '&#xe060;',
			'iconmoon-download-2' : '&#xe061;',
			'iconmoon-cloud' : '&#xe062;',
			'iconmoon-suitcase-3' : '&#xf0f2;',
			'iconmoon-envelope-2' : '&#xf003;',
			'iconmoon-heart' : '&#xe063;',
			'iconmoon-soccer' : '&#xe064;',
			'iconmoon-caret-down' : '&#xf0d7;',
			'iconmoon-caret-up' : '&#xf0d8;',
			'iconmoon-caret-left' : '&#xf0d9;',
			'iconmoon-caret-right' : '&#xf0da;',
			'iconmoon-circle-arrow-up' : '&#xf0aa;',
			'iconmoon-circle-arrow-down' : '&#xf0ab;',
			'iconmoon-chevron-up' : '&#xf077;',
			'iconmoon-chevron-down' : '&#xf078;',
			'iconmoon-chevron-right' : '&#xf054;',
			'iconmoon-chevron-left' : '&#xf053;',
			'iconmoon-thumbs-up' : '&#xf087;',
			'iconmoon-thumbs-down' : '&#xf088;',
			'iconmoon-heart-2' : '&#xf004;',
			'iconmoon-star' : '&#xf005;',
			'iconmoon-star-empty' : '&#xf006;',
			'iconmoon-lock' : '&#xf023;',
			'iconmoon-user' : '&#xf007;',
			'iconmoon-shopping-cart' : '&#xf07a;',
			'iconmoon-cloudy' : '&#xe002;',
			'iconmoon-gamepad' : '&#xe003;',
			'iconmoon-mouse' : '&#xe004;',
			'iconmoon-medkit' : '&#xf0fa;',
			'iconmoon-wrench' : '&#xf0ad;',
			'iconmoon-rss' : '&#xf09e;',
			'iconmoon-credit' : '&#xf09d;',
			'iconmoon-clipboard' : '&#xe005;',
			'iconmoon-credit-card' : '&#xe006;',
			'iconmoon-barbell' : '&#xe007;',
			'iconmoon-clipboard-2' : '&#xe008;',
			'iconmoon-clipboard-3' : '&#xe001;',
			'iconmoon-clipboard-4' : '&#xe009;',
			'iconmoon-copy' : '&#xe00a;'
		},
		els = document.getElementsByTagName('*'),
		i, attr, html, c, el;
	for (i = 0; ; i += 1) {
		el = els[i];
		if(!el) {
			break;
		}
		attr = el.getAttribute('data-icon');
		if (attr) {
			addIcon(el, attr);
		}
		c = el.className;
		c = c.match(/iconmoon-[^\s'"]+/);
		if (c && icons[c[0]]) {
			addIcon(el, icons[c[0]]);
		}
	}
};