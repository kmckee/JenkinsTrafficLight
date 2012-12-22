Feature: Display Current Build Status

@pending
Scenario: Current build is successful
	Given the current build is successful
	When the light updates
	Then the "green" light should be on

@pending
Scenario: Current build is broken
	Given the current build failed
	When the light updates
	Then the "red" light should be on

@pending
Scenario: Broken and building
    Given the previous build failed and a build is currently in progress
    When the light updates
    Then the "yellow" light should be on

@pending
Scenario: Unable to retrieve current build status
    When an error occurs retrieving the build status
    Then the "yellow" light should be blinking quickly