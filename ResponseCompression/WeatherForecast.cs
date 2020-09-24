using System;

namespace ResponseCompression
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }

        public string Description => "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque mi orci, fermentum tempor libero eget, convallis luctus urna. Vestibulum at tellus lorem. Curabitur eget leo ornare, ultrices orci sit amet, auctor quam. Aliquam nunc elit, elementum ut venenatis nec, lacinia at sem. Suspendisse in augue scelerisque, sodales metus ut, eleifend risus. Nullam mauris nibh, pulvinar vitae suscipit eget, blandit et enim. Vestibulum ipsum leo, suscipit at vulputate vitae, malesuada in nibh. Nullam porttitor est et leo tempor, a pretium enim ultricies. Integer accumsan lectus ac porttitor interdum. Donec euismod ut ligula in placerat. Integer orci quam, varius vitae arcu nec, mattis auctor odio. Curabitur pretium neque quis tempor venenatis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Curabitur ullamcorper mollis orci, in dapibus justo porttitor non. Mauris eu metus leo. Maecenas pulvinar elementum laoreet.Cras egestas arcu libero, a condimentum leo pretium nec.Donec sed rutrum mauris. Curabitur hendrerit venenatis mi et tempus. Vivamus tincidunt diam a tempor scelerisque. Sed rutrum nec nunc a accumsan. Praesent mattis mollis gravida. Nunc scelerisque rhoncus dui, in iaculis libero bibendum ut. Duis congue sed est at ornare. Donec imperdiet in enim sit amet aliquam. Donec vehicula gravida turpis.";
    }
}
