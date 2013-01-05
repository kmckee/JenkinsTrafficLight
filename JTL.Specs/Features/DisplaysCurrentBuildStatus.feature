﻿Feature: Display Current Build Status

Scenario: Current build is successful
	When the current build is successful
	Then the "green" light should be on


Scenario: Current build is broken
	When the current build failed
	Then the "red" light should be on

@ignore
Scenario: Broken and building
    Given the last build failed
    When a build is currently in progress
    Then the "yellow" light should be on

@ignore
Scenario: Unable to retrieve current build status
    When an error occurs retrieving the build status
    Then the "yellow" light should be blinking