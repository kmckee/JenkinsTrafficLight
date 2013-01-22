require 'json'
require 'restclient'

module JenkinsLight
  class JenkinsFeed
    attr_accessor :url
    def initialize url
      @url = url
    end
    
    def get_status
      begin
        status = JSON.parse(RestClient.get(api_url))['color']
      rescue RestClient::Forbidden 
        status = 'auth'
      rescue
        status = 'error'
      end

      { 'blue' => {:status => 'Green', :details => ''},
        'red' => {:status => 'Red', :details => 'Build failed'},
        'disabled' => {:status => 'Unknown', :details => 'Jenkins is suspended'},
        'yellow' => {:status => 'Red', :details => 'Failing tests'},
        'red_anime' => {:status => 'Yellow', :details => 'Broken and building...'},
        'yellow_anime' => {:status => 'Yellow', :details => 'Test failure(s) on previous build, rebuilding...'},
        'error' => {:status => 'Unknown', :details => 'Error contacting Jenkins (404)'},
        'auth' => {:status => 'Unknown', :details => 'Authentication Required (403)', :code => :auth}
      }[status]
    end

    private

    def api_url
      @url + "/api/json"
    end

  end
end
