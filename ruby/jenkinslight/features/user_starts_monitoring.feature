Feature: User starts monitoring a build
  As a user
  I want to start monitoring a build
  So that my traffic light will display the current status of the build

  Scenario: Start the monitor app
    Given I am not yet monitoring a build
    When I start the JenkinsLight monitor
    Then I should see "Jenkins Build Light Monitor Started"
    And I should see "Enter the URL of the Jenkins job to monitor:"
