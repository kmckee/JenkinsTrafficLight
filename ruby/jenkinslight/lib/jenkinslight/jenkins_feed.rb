require 'json'
require 'restclient'

module JenkinsLight
  class JenkinsFeed
    attr_accessor :url, :username, :password
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

      { 'blue' => {:status => :green, :details => ''},
        'red' => {:status => :red, :details => 'Build failed'},
        'disabled' => {:status => :unknown, :details => 'Jenkins is suspended'},
        'yellow' => {:status => :red, :details => 'Failing tests'},
        'blue_anime' => {:status => :green, :details => ''},
        'red_anime' => {:status => :yellow, :details => 'Broken and building...'},
        'yellow_anime' => {:status => :yellow, :details => 'Test failure(s) on previous build, rebuilding...'},
        'error' => {:status => :unknown, :details => 'Error contacting Jenkins (404)'},
        'auth' => {:status => :unknown, :details => 'Authentication Required (403)', :code => :auth}
      }[status]
    end

    def api_url
      temp = @url.strip
      temp << "/" unless temp.end_with?("/")
      temp = temp + "api/json"

      if (username.nil? == false)
        temp = temp.sub("://", "://#{username}:#{password}@")
      end

      temp
    end

  end
end
