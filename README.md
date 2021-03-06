<div id="top"></div>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![GPL License][license-shield]][license-url]


<h3 align="center">RandTester</h3>

  <p align="center">
    PRNG Tester GUI
    <br />
    Started as a project to learn about PRNG algorithms.
    <br />
    May end up as a GUI alternative to test suites such as Diehard(er), TestU01(BigCrush), PractRand, etc.
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

[![Product Name Screen Shot][product-screenshot]](https://github.com/paultron/RandTester)
Version 1 lets you input values into the four 64-bit state array and view the resulting outputs.
Currently only using Xoshiro256++
Eventually you will be able to choose the algorithm, and output to a file. 
<p align="right">(<a href="#top">back to top</a>)</p>



### Built With

* C# .Net Core 6.0
* Windows Forms
* Love

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

Compiled in Visual Studio 22 with no external resources. YMMV.


<!-- USAGE EXAMPLES -->
## Usage

For now, open and fill state boxes then click 'Mix All' to see the output

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- ROADMAP -->
## Roadmap

- [ ] Random data output to file(s)
- [ ] Random data input for viewing/testing
- [ ] Tests
- [ ] Charts
    - [ ] Lots of charts
- [ ] More algorithms to choose from
See the [open issues](https://github.com/paultron/RandTester/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the GNU GPL 3.0 License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Paul Larkin - palark93@gmail.com

Project Link: [https://github.com/paultron/RandTester](https://github.com/paultron/RandTester)

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/paultron/RandTester.svg?style=for-the-badge
[contributors-url]: https://github.com/paultron/RandTester/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/paultron/RandTester.svg?style=for-the-badge
[forks-url]: https://github.com/paultron/RandTester/network/members
[stars-shield]: https://img.shields.io/github/stars/paultron/RandTester.svg?style=for-the-badge
[stars-url]: https://github.com/paultron/RandTester/stargazers
[issues-shield]: https://img.shields.io/github/issues/paultron/RandTester.svg?style=for-the-badge
[issues-url]: https://github.com/paultron/RandTester/issues
[license-shield]: https://img.shields.io/github/license/paultron/RandTester.svg?style=for-the-badge
[license-url]: https://github.com/paultron/RandTester/blob/master/LICENSE.txt
[product-screenshot]: images/screenshot2.png
