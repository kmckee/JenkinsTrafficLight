Feature: Monitor Jenkins jobs that require authentication
  As a user
  I want to be able to monitor jobs that require basic authentication
  So that I can tell if the app is working

  Scenario: Prompt for credentials when needed
    When I monitor a build that requires basic authentication
    Then I should see a message with the current time and "Build Status: Unknown\\tAuthentication Required"
    And I should be prompted to enter a user name
    And I should be prompted to enter a password
